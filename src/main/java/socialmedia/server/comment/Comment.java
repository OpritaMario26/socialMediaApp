package socialmedia.server.comment;

import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.CreationTimestamp;
import socialmedia.server.post.Post;
import socialmedia.server.user.User;

import java.util.Date;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Comment {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    private String content;

    @CreationTimestamp
    @Column(updatable = false, nullable = false)
    private Date createdOn;

    @ManyToOne
    @JoinColumn(name="user_id", nullable = true)
    private User user;

    @ManyToOne
    @JoinColumn(name="post_id", nullable = true)
    private Post post;


}
