using FinalProjectSOA.API.Controllers;
using FinalProjectSOA.API.Data;
using FinalProjectSOA.API.Models.DTOs;
using FinalProjectSOA.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FinalProjectSOA.API.Tests
{
    public class PostsControllerTests
    {
        [Fact]
        public async Task GetAllPosts_ReturnsOkResultWithPosts()
        {
            // Arrange
            var dbContext = GetDbContext();
            var controller = new PostsController(dbContext);

            // Act
            var result = await controller.GetAllPosts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var posts = Assert.IsAssignableFrom<List<Post>>(okResult.Value);
            Assert.Equal(2, posts.Count); // Assuming there are 2 posts in the database
        }

        [Fact]
        public async Task GetPostsById_WithValidId_ReturnsOkResultWithPost()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var dbContext = GetDbContext();
            dbContext.Posts.Add(new Post { Id = postId });
            dbContext.SaveChanges();
            var controller = new PostsController(dbContext);

            // Act
            var result = await controller.GetPostsById(postId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var post = Assert.IsType<Post>(okResult.Value);
            Assert.Equal(postId, post.Id);
        }

        [Fact]
        public async Task GetPostsById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var dbContext = GetDbContext();
            var controller = new PostsController(dbContext);

            // Act
            var result = await controller.GetPostsById(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddPost_WithValidPost_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var dbContext = GetDbContext();
            var controller = new PostsController(dbContext);
            var addPostRequest = new AddPostRequest
            {
                Title = "Test Title",
                Content = "Test Content",
                Author = "Test Author",
                FeaturedImageUrl = "https://example.com/image.jpg",
                PublishDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Summary = "Test Summary",
                UrlHandle = "test-post",
                Visible = true
            };

            // Act
            var result = await controller.AddPost(addPostRequest);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var post = Assert.IsType<Post>(createdAtActionResult.Value);
            Assert.NotNull(post.Id);
            Assert.Equal(addPostRequest.Title, post.Title);
            Assert.Equal(addPostRequest.Content, post.Content);
            Assert.Equal(addPostRequest.Author, post.Author);
            Assert.Equal(addPostRequest.FeaturedImageUrl, post.FeaturedImageUrl);
            Assert.Equal(addPostRequest.PublishDate, post.PublishDate);
            Assert.Equal(addPostRequest.UpdatedDate, post.UpdatedDate);
            Assert.Equal(addPostRequest.Summary, post.Summary);
            Assert.Equal(addPostRequest.UrlHandle, post.UrlHandle);
            Assert.Equal(addPostRequest.Visible, post.Visible);
        }



        [Fact]
        public async Task UpdatePost_WithExistingPost_ReturnsOkResultWithUpdatedPost()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var dbContext = GetDbContext();
            dbContext.Posts.Add(new Post { Id = postId });
            dbContext.SaveChanges();
            var controller = new PostsController(dbContext);
            var updatePostRequest = new UpdatePostRequest { /* set the properties for the request */ };

            // Act
            var result = await controller.UpdatePost(postId, updatePostRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var post = Assert.IsType<Post>(okResult.Value);
            Assert.Equal(postId, post.Id);
            // Add assertions for updated properties
        }

        [Fact]
        public async Task UpdatePost_WithNonExistingPost_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var dbContext = GetDbContext();
            var controller = new PostsController(dbContext);
            var updatePostRequest = new UpdatePostRequest { /* set the properties for the request */ };

            // Act
            var result = await controller.UpdatePost(invalidId, updatePostRequest);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePost_WithExistingPost_ReturnsOkResultWithDeletedPost()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var dbContext = GetDbContext();
            dbContext.Posts.Add(new Post { Id = postId });
            dbContext.SaveChanges();
            var controller = new PostsController(dbContext);

            // Act
            var result = await controller.DeletePost(postId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var post = Assert.IsType<Post>(okResult.Value);
            Assert.Equal(postId, post.Id);
        }

        [Fact]
        public async Task DeletePost_WithNonExistingPost_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var dbContext = GetDbContext();
            var controller = new PostsController(dbContext);

            // Act
            var result = await controller.DeletePost(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private AppDbContext GetDbContext()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new AppDbContext(options);

            // Seed the database with test data
            dbContext.Posts.AddRange(new List<Post>
            {
                new Post { Id = Guid.NewGuid() },
                new Post { Id = Guid.NewGuid() }
            });
            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
