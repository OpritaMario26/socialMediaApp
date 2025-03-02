<template>
  <v-app>
    <v-main>
      <add-user
          ref="addUserDialog"
      ></add-user>
      <v-btn style="align-content: center;margin-left: 50px;margin-top: 20px" @click="openDialogForUser">
        Register
      </v-btn>
      <div></div>
      <login
          ref="userLoginDialog">
      </login>
      <v-btn style="align-content: center;margin-left: 50px;margin-top: 20px" @click="openLoginDialog">
        Log In
      </v-btn>

      <!--      <HelloWorld/>-->
      <div style="max-width: 1000px;margin-left: 200px">
        <v-btn v-if="isUserLogged()" style="align-content:center" @click="openDialog">
          Create new post
        </v-btn>
        <posts-view/>
      </div>
      <add-post
          :currentUser="username"
          ref="addPostDialog"
      ></add-post>
    </v-main>
  </v-app>
</template>

<script>
import PostsView from "@/components/PostsView.vue";
import AddPost from "@/components/AddPost.vue";
import AddUser from "@/components/AddUser.vue";
import Login from "@/components/Login.vue";
import axios from "axios";

export default {
  name: 'App',

  components: {
    PostsView,
    AddPost,
    AddUser,
    Login
  },

  data: () => ({
    username: "",
    id: ""
  }),

  created() {
    // Căutăm userId în localStorage la început
    const userEmail = localStorage.getItem('userEmail');

    if (userEmail) {
      // Dacă există userId în localStorage, recuperăm informațiile utilizatorului
      this.getUserData(userEmail);
    }
  },


  methods: {
    openDialog() {
      this.$refs.addPostDialog.showDialog = true
    },
    openDialogForUser(){
      this.$refs.addUserDialog.showDialog = true
    },
    openLoginDialog() {
      this.$refs.userLoginDialog.showDialog = true
    },
    async getUserData(userEmail) {
      try {

        // Trimitem cererea către server pentru a obține datele utilizatorului
        const response = await axios.get(`http://localhost:8083/user/login?email=${userEmail}`);

        if (response.data) {
          // Dacă am obținut datele utilizatorului, actualizăm numele în aplicație
          this.username = response.data.name;
          this.id = response.data.id;

        } else {
          console.log("User not found");
        }
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    },
    isUserLogged(){
      if(this.id) {
        return true
      }
    },



  }


}
</script>