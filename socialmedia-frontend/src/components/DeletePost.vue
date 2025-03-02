<template>
  <v-dialog v-model="showDialog" max-width="400px" class="bg-transparent">
    <div class="dialogClass">
      Are you sure you want to delete the post titled: "{{postTitle}}"?
      <v-btn color="red" @click="deletePost">Delete</v-btn>
      <v-btn color="green" @click="closeDialog">Cancel</v-btn>
    </div>
  </v-dialog>
</template>

<script>
import axios from 'axios';

export default {
  name: 'DeletePost',
  props: {
    postId: Number,  // ID-ul postului care va fi șters
    postTitle: String,  // Titlul postului care va fi șters
  },
  data() {
    return {
      showDialog: false,  // Control pentru afisarea dialogului
    };
  },
  methods: {
    async deletePost() {
      try {
        const response = await axios.delete(`http://localhost:8083/post/${this.postId}`);
        console.log("Post deleted successfully:", response.data);
        this.showDialog = false;
        this.$emit('postDeleted');  // Emiterea unui eveniment pentru a actualiza lista postărilor
      } catch (error) {
        console.error("Error deleting post:", error);
      }
    },
    closeDialog() {
      this.showDialog = false;
    },
  }
};
</script>

<style scoped>
.dialogClass {
  padding: 20px;
  background-color: white;
}
</style>
