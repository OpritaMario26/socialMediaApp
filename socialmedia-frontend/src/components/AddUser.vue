<script>
import axios from 'axios';

export default {

  name: 'users',
  data() {
    return {
      showDialog: false,
      user: {
        name: "",
        email: "",
      }
    }
  },
  methods: {
    async createUser() {
      try {
        const response = await axios.post('http://localhost:8083/user', this.user)
        console.log("user created successfully:", response.data);
        this.showDialog = false
      } catch (error) {
        console.error("Error creating post:", error);
        alert("Email already in use");
      }
    },
    closeDialog() {
      this.showDialog = false;
      this.post = { name: "", email: "" };
    },
  }
};
</script>

<template>
  <v-dialog v-model="showDialog" max-width="400px" class="bg-transparent">
    <div class="dialogClass">
      Please enter a Name and Email
      <v-text-field
          label="Name"
          v-model="user.name"
      ></v-text-field>
      <v-textarea
          label="Email"
          v-model="user.email"
      ></v-textarea>
      <v-btn color="green" @click="createUser">Create</v-btn>
      <v-btn color="red" @click="closeDialog">Cancel</v-btn>
    </div>
  </v-dialog>
</template>

<style scoped>
.dialogClass {
  padding: 20px;
  background-color: white;
}
</style>