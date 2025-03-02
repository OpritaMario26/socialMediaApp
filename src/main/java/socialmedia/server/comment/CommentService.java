package socialmedia.server.comment;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import socialmedia.server.post.Post;
import socialmedia.server.user.User;
import socialmedia.server.user.UserRepository;

import java.util.List;

@Service
public class CommentService {

    @Autowired
    private CommentRepository commentRepository;

    @Transactional
    public Comment create(Comment myComment) {
        return commentRepository.save(myComment);
    }

    public List<Comment> findAllComments(){
        return commentRepository.findAll();
    }

    public void deleteCommentById(int id) {
        if (commentRepository.existsById(id)) {
            commentRepository.deleteById(id);
        } else {
            throw new RuntimeException("Comment not found with ID: " + id);
        }
    }

    public Comment updateComment(int id, Comment updatedComment) {
        return commentRepository.findById(id).map(existingComment -> {
            if (updatedComment.getContent() != null) {
                existingComment.setContent(updatedComment.getContent());
            }
            return commentRepository.save(existingComment);
        }).orElseThrow(() -> new RuntimeException("Post not found with ID: " + id));
    }

    public List<Comment> getAllCommentsForPost(Integer postId) {
        return commentRepository.findAllCommentsForPost(postId);
    }


}
