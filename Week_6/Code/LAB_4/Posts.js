import React from 'react';

class Posts extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      posts: []
    };
  }

  componentDidMount() {
    fetch("https://dummyjson.com/posts")
      .then(res => res.json())
      .then(data => {
        this.setState({ posts: data.posts });
      })
      .catch(err => {
        alert("Failed to load posts");
        console.log(err);
      });
  }

  render() {
    return (
      <div>
        <h2>Blog Posts</h2>
        {this.state.posts.map((post) => (
          <div key={post.id} style={{ marginBottom: '20px' }}>
            <h4>{post.title}</h4>
            <p>{post.body}</p>
          </div>
        ))}
      </div>
    );
  }
}

export default Posts;
