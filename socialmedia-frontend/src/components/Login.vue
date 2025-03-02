<template>
  <v-dialog v-model="showDialog" max-width="400px" class="bg-transparent">
    <div class="dialogClass">
      Please enter an Email
      <v-textarea
          label="Email"
          v-model="user.email"
      ></v-textarea>
      <v-btn color="green" @click="checkUserEmail">Login</v-btn>
      <v-btn color="red" @click="closeDialog">Cancel</v-btn>
    </div>
  </v-dialog>
</template>

<script>
import axios from 'axios';

export default {
  name: 'userLoged',
  data() {
    return {
      showDialog: false,
      user: {
        email: "",
      },
    };
  },
  methods: {
    async checkUserEmail() {
      if (!this.user.email.trim()) {
        alert("Please enter a valid email");
        return;
      }

      try {
        // Trimite cererea către server pentru a verifica email-ul
        const response = await axios.get(`http://localhost:8083/user/login?email=${this.user.email}`);

        if (response.data && response.data.email) {
          // Dacă email-ul există în baza de date, salvăm userId în localStorage
          localStorage.setItem('userEmail', response.data.email);
          localStorage.setItem('userId', response.data.id);
          window.location.reload();
          console.log("User Email saved:", response.data.email);
          console.log("Local storage variable",localStorage.getItem('userEmail'))

          // Poți folosi userId în toată aplicația din localStorage
          this.showDialog = false; // Închide dialogul după login
          alert("Logged in successfully");
        } else {
          alert("Email not found.");
        }
      } catch (error) {
        console.error("Error during login:", error);
        alert("An error occurred. Please try again.");
      }
    },
    closeDialog() {
      this.showDialog = false;
      this.user.email = ""; // Resetăm câmpul email
    },
  },
};
</script>

<style scoped>
.dialogClass {
  padding: 20px;
  background-color: white;
}
</style>
