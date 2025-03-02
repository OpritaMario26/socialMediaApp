<template>
  <v-container>
    <h2 class="text-h5">Posts</h2>
    <v-btn text="Load Posts" @click="fetchPosts"></v-btn>

    <!-- Afișăm lista de posturi doar după ce au fost încărcate -->
    <v-list v-if="posts.length > 0">
      <v-list-item
          class="postClass"
          variant="outlined"
          v-for="post in paginatedPosts.filter(post => post.status ==='PUBLISHED')"
          :key="post.id"
          :subtitle="'posted on ' + post.createdOn"
          :title="post.title + ' BY ' + post.user.name"
      >
        {{ post.content }}
        <div style="margin-top: 10px;">
          <v-btn small text color="primary" @click="toggleComments(post.id)">
            {{ commentsVisible[post.id] ? 'Hide Comments' : 'Show Comments' }}
          </v-btn>
          <v-btn small text color="secondary" @click="openAddCommentDialog(post.id)">
            Add Comment
          </v-btn>
          <div style="text-align: right; width: 100%">
          <v-btn v-if="isUserOwner(post.user.email)" small text color="error" @click="confirmDelete(post.id, post.title)">
            Delete
          </v-btn>
          </div>
        </div>

        <!-- Comentarii -->
        <v-list v-if="commentsVisible[post.id]" class="commentsSection">
          <v-list-item v-for="comment in comments[post.id] || []" :key="comment.id" :title="comment.user.name">
            {{ comment.content }}
          </v-list-item>
          <v-btn small text color="primary" @click="loadComments(post.id)">
            Reload Comments
          </v-btn>
        </v-list>

        <!-- Dialog AddComment -->
        <add-comment
            v-if="addCommentVisible[post.id]"
            :post-id="post.id"
            @commentAdded="handleCommentAdded(post.id, $event)"
            @close="closeAddCommentDialog(post.id)"
        />
        <v-dialog v-model="showDeleteDialog" max-width="400px">
          <v-card>
            <v-card-title class="headline">Confirm Deletion</v-card-title>
            <v-card-text>
              Are you sure you want to delete the post titled: "{{ postTitle }}"?
            </v-card-text>
            <v-card-actions>
              <v-btn color="red" @click="deletePost">Delete</v-btn>
              <v-btn color="green" @click="closeDeleteDialog">Cancel</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>


      </v-list-item>
    </v-list>

    <!-- Paginarea este vizibilă doar dacă posturile au fost încărcate -->
    <v-pagination v-if="isPostsLoaded" v-model="currentPage" :length="totalPages" @input="updatePaginatedPosts"></v-pagination>
  </v-container>
</template>


<script>
import axios from "axios";
import AddComment from "./AddComment.vue";
export default {
  name: "PostsView",
  components: {
    AddComment,
  },
  data() {
    return {
      posts: [],
      comments: {}, // Comentarii per post
      commentsVisible: {}, // Starea de vizibilitate a comentariilor
      currentPage: 1, // Pagină curentă
      pageSize: 5, // Numărul de posturi per pagină
      totalPages: 0, // Numărul total de pagini
      paginatedPosts: [], // Posturile pentru pagina curentă
      isPostsLoaded: false, // Starea care controlează vizibilitatea paginării
      addCommentVisible: {}, // Stare pentru dialogurile AddComment per post
      showDeleteDialog: false, // Control pentru dialogul de ștergere
      postToDeleteId: null, // ID-ul postării de șters
      postTitle: "", // Titlul postării de șters
    };
  },
  methods: {
    async fetchPosts() {
      try {
        const response = await axios.get("http://localhost:8083/post");
        this.posts = response.data;
        this.totalPages = Math.ceil(this.posts.length / this.pageSize); // Calculăm numărul total de pagini
        this.isPostsLoaded = true; // Setăm că posturile au fost încărcate
        this.updatePaginatedPosts(); // Actualizăm posturile pentru pagina curentă
      } catch (error) {
        console.error("Error fetching posts:", error);
      }
    },
    async loadComments(postId) {
      try {
        const response = await axios.get(
            `http://localhost:8083/comment/post/${postId}`
        );
        this.comments = { ...this.comments, [postId]: response.data }; // Actualizează comentariile pentru postId
      } catch (error) {
        console.error(`Error loading comments for post ${postId}:`, error);
      }
    },
    updatePaginatedPosts() {
      const approvedPosts = this.posts.filter(post => post.status === 'PUBLISHED');
      this.totalPages = Math.ceil(approvedPosts.length / this.pageSize); // Recalculăm paginile
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      this.paginatedPosts = approvedPosts.slice(start, end);
    },
    toggleComments(postId) {
      if (!this.commentsVisible[postId]) {
        // Dacă comentariile nu sunt afișate, le afișează
        this.commentsVisible = {...this.commentsVisible, [postId]: true};
        // Dacă comentariile nu sunt încă încărcate, le încarcă
        if (!this.comments[postId]) {
          this.loadComments(postId);
        }
      } else {
        // Ascunde comentariile
        this.commentsVisible = {...this.commentsVisible, [postId]: false};
      }
    },
    handleCommentAdded(postId, comment) {
      console.log("Comment added for post:", postId, comment); // Debugging
      this.comments = {
        ...this.comments,
        [postId]: [...(this.comments[postId] || []), comment],
      };
    },
    openAddCommentDialog(postId) {
      console.log("Opening AddComment dialog for post:", postId); // Debugging
      this.addCommentVisible = { ...this.addCommentVisible, [postId]: true };
    },
    closeAddCommentDialog(postId) {
      console.log("Closing AddComment dialog for post:", postId); // Debugging
      this.addCommentVisible = { ...this.addCommentVisible, [postId]: false };
    },
    confirmDelete(postId, postTitle) {
      this.postToDeleteId = postId;
      this.postTitle = postTitle;
      this.showDeleteDialog = true;
    },
    // Șterge postarea
    async deletePost() {
      try {
        await axios.delete(`http://localhost:8083/post/${this.postToDeleteId}`);
        console.log("Post deleted successfully");
        this.showDeleteDialog = false;
        this.fetchPosts(); // Actualizăm lista de postări după ștergere
      } catch (error) {
        console.error("Error deleting post:", error);
      }
    },
    closeDeleteDialog() {
      this.showDeleteDialog = false;
    },
    isUserOwner(postUserEmail){
      const currentUser = localStorage.getItem("userEmail");
      return currentUser === postUserEmail
    }
  },
  watch: {
    // Când pagina se schimbă, actualizăm posturile
    currentPage() {
      this.updatePaginatedPosts();
    }
  }
};
</script>

<style scoped>
.postClass {
  margin-bottom: 10px;
}

.commentsSection {
  margin-left: 20px;
  background: white;
  padding: 10px;
  border-radius: 8px;
}
</style>
