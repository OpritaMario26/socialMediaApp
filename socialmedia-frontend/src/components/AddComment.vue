<template>
  <v-dialog v-model="dialogVisible" persistent max-width="400">
    <v-card>
      <v-card-title>
        <span class="text-h5">Add a Comment</span>
      </v-card-title>
      <v-card-text>
        <v-textarea v-model="newComment" label="Your Comment"></v-textarea>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" text @click="submitComment">Submit</v-btn>
        <v-btn color="secondary" text @click="closeDialog">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import axios from "axios";

export default {
  props: ["postId"],
  data() {
    return {
      dialogVisible: true,
      newComment: "",
    };
  },
  methods: {
    async submitComment() {
      if (!this.newComment.trim()) {
        alert("Comment cannot be empty!");
        return;
      }

      const comment = {
        content: this.newComment,
        post: {
          id: this.postId,  // Aici adăugăm obiectul post cu id-ul
        },
        user: {
          id: Number(localStorage.getItem("userId")),
        },
      };

      try {
        const response = await axios.post("http://localhost:8083/comment", comment);
        console.log("Comment created successfully:", response.data);
        this.$emit("commentAdded", response.data); // Trimitem comentariul către parent
        this.closeDialog();
      } catch (error) {
        console.error("Error creating comment:", error);
        alert("An error occurred while adding the comment.");
      }
    },
    closeDialog() {
      this.$emit("close"); // Semnalizăm închiderea dialogului
    },
  },
};
</script>
