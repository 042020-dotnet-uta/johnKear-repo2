using CharSheet.Data;
using CharSheet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using Xunit;
using System.Collections.Generic;

namespace CharSheet.Test
{
	public class RepositoryTestsGeneric : RepositoryTests
	{
		[Fact]
        public async void RepositoryAll()
        {
            // Arrange
            var options = GetOptions("RepositoryAll");
            InsertUser(options);
            IEnumerable<User> users;

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                users = await unitOfWork.UserRepository.All();
            }

            // Assert
            Assert.Single(users);
        }

        [Fact]
        public async void RepositoryInsert()
        {
            // Arrange
            var options = GetOptions("RepositoryInsert");
            
            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.UserRepository.Insert(new User());
                await unitOfWork.Save();
            }

            // Assert
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var users = await unitOfWork.UserRepository.All();
                Assert.Single(users);
            }
        }

        [Fact]
        public async void RepositoryUpdate()
        {
            // Arrange
            var options = GetOptions("RepositoryUpdate");
            InsertUser(options);

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var users = await unitOfWork.UserRepository.All();
                var user = users.First();
                user.Username = "Something hi";
                await unitOfWork.UserRepository.Update(user);
                await unitOfWork.Save();
            }

            // Assert
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var users = await unitOfWork.UserRepository.All();
                var user = users.First();
                Assert.Equal("Something hi", user.Username);
            }
        }

        [Fact]
        public async void RepositoryFind()
        {
            // Arrange
            var options = GetOptions("RepositoryFind");
            Guid id;
            User user = null;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var newUser = await unitOfWork.UserRepository.Insert(new User());
                await unitOfWork.Save();
                id = newUser.UserId;
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                user = await unitOfWork.UserRepository.Find(id);
            }

            // Assert
            using (var unitOfWork = GetUnitOfWork(options))
            {
                Assert.NotNull(user);
            }
        }

        [Fact]
        public async void RepositoryGet()
        {
            // Arrange
            var options = GetOptions("RepositoryGet");
            User user = null;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var newUser = await unitOfWork.UserRepository.Insert(new User
                {
                    Username = "Something"
                });
                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var users = await unitOfWork.UserRepository.Get(u => u.Username == "Something", null, null);
                user = users.First();
            }

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async void RepositoryRemove()
        {
            // Arrange
            var options = GetOptions("RepositoryRemove");
            InsertUser(options);

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var users = await unitOfWork.UserRepository.All();
                var user = users.First();
                await unitOfWork.UserRepository.Remove(user.UserId);
                await unitOfWork.Save();
            }

            // Assert
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var users= await unitOfWork.UserRepository.All();
                Assert.Empty(users);
            }
        }
	}
}
