package socialmedia.server.post;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.List;

@Service
public class PostService {

    @Autowired
    private PostRepository postRepository;

    @Transactional
    public Post create(Post myPost) {
        return postRepository.save(myPost);
    }

    public List<Post> findAllPosts(){
        return postRepository.findAll();
    }

    public void deletePostById(int id) {
        if (postRepository.existsById(id)) {
            postRepository.deleteById(id);
        } else {
            throw new RuntimeException("Post not found with ID: " + id);
        }
    }

    public Post updatePost(int id, Post updatedPost) {
        return postRepository.findById(id).map(existingPost -> {
            if (updatedPost.getTitle() != null) {
                existingPost.setTitle(updatedPost.getTitle());
            }
            if (updatedPost.getContent() != null) {
                existingPost.setContent(updatedPost.getContent());
            }
            if (updatedPost.getStatus() != null) {
                existingPost.setStatus(updatedPost.getStatus());
            }
            return postRepository.save(existingPost);
        }).orElseThrow(() -> new RuntimeException("Post not found with ID: " + id));
    }

    public List<Post> getAllPublishedPostsForUser(Integer userId) {
        return postRepository.findAllPublishedPostsForUser(userId);
    }

    public List<Post> searchPostsByKeyword(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return new ArrayList<>();
        }


        return postRepository.searchPostsByKeyword(keyword);
    }

}
