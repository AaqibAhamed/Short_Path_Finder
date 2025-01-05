using ShortestPath.Core.Entities;
using ShortestPath.Core.Repositories;

namespace ShortestPath.Tests;

using System.Collections.Generic;
using Xunit;
public class ShortPathRepositoryTests
{
    [Fact]
        public void FindShortestPath_SimpleGraph_ReturnsCorrectPathAndDistance()
        {
            // Arrange
            var graph = new List<Node>
            {
                new Node
                {
                    Name = "A",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "B" }, Weight = 1 },
                        new Edge { TargetNode = new Node { Name = "C" }, Weight = 4 }
                    }
                },
                new Node
                {
                    Name = "B",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "C" }, Weight = 2 },
                        new Edge { TargetNode = new Node { Name = "D" }, Weight = 5 }
                    }
                },
                new Node
                {
                    Name = "C",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "D" }, Weight = 1 }
                    }
                },
                new Node
                {
                    Name = "D",
                    Edges = new List<Edge>()
                }
            };

            var repository = new ShortPathRepository();

            // Act
            var result = repository.FindShortestPath("A", "D", graph);

            // Assert
            Assert.Equal(new List<string> { "A", "B", "C", "D" }, result.NodeNames);
            Assert.Equal(4, result.Distance);
        }

        [Fact]
        public void FindShortestPath_NoPathAvailable_ThrowsException()
        {
            // Arrange
            var graph = new List<Node>
            {
                new Node
                {
                    Name = "A",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "B" }, Weight = 1 }
                    }
                },
                new Node
                {
                    Name = "B",
                    Edges = new List<Edge>()
                },
                new Node
                {
                    Name = "C",
                    Edges = new List<Edge>()
                }
            };

            var repository = new ShortPathRepository();

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => repository.FindShortestPath("A", "D", graph));
            Assert.Contains("The given key 'D' was not present in the dictionary", exception.Message);
        }

        [Fact]
        public void FindShortestPath_SingleNodeGraph_ReturnsZeroDistance()
        {
            // Arrange
            var graph = new List<Node>
            {
                new Node
                {
                    Name = "A",
                    Edges = []
                }
            };

            var repository = new ShortPathRepository();

            // Act
            var result = repository.FindShortestPath("A", "A", graph);

            // Assert
            Assert.Equal([], result.NodeNames);
            Assert.Equal(0, result.Distance);
        }

        [Fact]
        public void FindShortestPath_MultiplePaths_ReturnsShortest()
        {
            // Arrange
            var graph = new List<Node>
            {
                new Node
                {
                    Name = "A",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "B" }, Weight = 2 },
                        new Edge { TargetNode = new Node { Name = "C" }, Weight = 1 }
                    }
                },
                new Node
                {
                    Name = "B",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "D" }, Weight = 2 }
                    }
                },
                new Node
                {
                    Name = "C",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "D" }, Weight = 1 }
                    }
                },
                new Node
                {
                    Name = "D",
                    Edges = new List<Edge>()
                }
            };

            var repository = new ShortPathRepository();

            // Act
            var result = repository.FindShortestPath("A", "D", graph);

            // Assert
            Assert.Equal(new List<string> { "A", "C", "D" }, result.NodeNames);
            Assert.Equal(2, result.Distance);
        }

        [Fact]
        public void FindShortestPath_MissingNode_ThrowsException()
        {
            // Arrange
            var graph = new List<Node>
            {
                new Node
                {
                    Name = "A",
                    Edges = new List<Edge>
                    {
                        new Edge { TargetNode = new Node { Name = "B" }, Weight = 1 }
                    }
                },
                new Node
                {
                    Name = "B",
                    Edges = new List<Edge>()
                }
            };

            var repository = new ShortPathRepository();

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => repository.FindShortestPath("A", "C", graph));
            Assert.Contains("The given key 'C' was not present in the dictionary.", exception.Message);
        }


        
    }
