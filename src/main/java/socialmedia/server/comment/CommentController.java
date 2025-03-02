package socialmedia.server.comment;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import socialmedia.server.post.Post;


import java.util.List;

@RestController
@RequestMapping("/comment")
@CrossOrigin
public class CommentController {

    @Autowired
    private CommentService commentService;

    @PostMapping
    public Comment create(@RequestBody Comment myComment){
        return commentService.create(myComment);
    }

    @GetMapping
    public List<Comment> getAllComments(){
        return commentService.findAllComments();
    }

    @DeleteMapping("/{id}")
    public String deleteComment(@PathVariable int id){
        commentService.deleteCommentById(id);
        return "Comment with ID " + id + " was deleted successfully.";
    }

    @PutMapping("/{id}")
    public Comment updateComment(@PathVariable int id, @RequestBody Comment updatedComment) {
        return commentService.updateComment(id, updatedComment);
    }

    @GetMapping("/post/{postId}")
    public List<Comment> getAllCommentsForPost(@PathVariable Integer postId) {
        return commentService.getAllCommentsForPost(postId);
    }

}