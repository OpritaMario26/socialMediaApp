package socialmedia.server.user;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import socialmedia.server.post.Post;


import java.util.List;

@RestController
@RequestMapping("/user")
@CrossOrigin
public class UserController {

    @Autowired
    private UserService userService;

    @PostMapping
    public User create(@RequestBody User myUser){
       return userService.create(myUser);
    }

    @GetMapping
    public List<User> getAllUsers(){
        return userService.findAllUsers();
    }


    @GetMapping("/login")
    public ResponseEntity<User> searchUserByEmail(@RequestParam String email) {
        if (email == null || email.trim().isEmpty()) {
            return ResponseEntity.badRequest().body(null); // Email-ul este invalid
        }

        User user = userService.findUserByEmail(email); // Căutăm utilizatorul după email

        if (user == null) {
            return ResponseEntity.notFound().build(); // Nu am găsit utilizatorul
        }

        return ResponseEntity.ok(user); // Returnăm utilizatorul găsit
    }

}
