package socialmedia.server.post;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/post")
@CrossOrigin
public class PostController {

    @Autowired
    private PostService postService;

    @PostMapping
    public Post create(@RequestBody Post myPost)
    {
        return postService.create(myPost);
    }

    @GetMapping
    public List<Post> getAllPosts(){
        return postService.findAllPosts();
    }

    @DeleteMapping("/{id}")
    public String deletePost(@PathVariable int id){
        postService.deletePostById(id);
        return "Post with ID " + id + " was deleted successfully.";
    }

    @PutMapping("/{id}")
    public Post updatePost(@PathVariable int id, @RequestBody Post updatedPost) {
        return postService.updatePost(id, updatedPost);
    }

    @GetMapping("/user/{userId}/published")
    public List<Post> getAllPublishedPostsForUser(@PathVariable Integer userId) {
        return postService.getAllPublishedPostsForUser(userId);
    }

    @GetMapping("/search")
    public ResponseEntity<List<Post>> searchPostsByKeyword(@RequestParam String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return ResponseEntity.badRequest().body(null);
        }

        List<Post> posts = postService.searchPostsByKeyword(keyword);

        if (posts.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(posts);
    }


}
