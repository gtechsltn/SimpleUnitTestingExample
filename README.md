# Simple Unit Testing Example

https://github.com/gtechsltn/game-character-api

https://github.com/gtechsltn/SimpleUnitTestingExample

How to add source code to GitHub

https://docs.google.com/document/d/1IK3qYmw-mcx6CXCG_RZIOlpAGKUclB1pqKgKD0ncgWY

```
git init
git remote add origin https://github.com/gtechsltn/SimpleUnitTestingExample.git
git remote set-url origin https://github.com/gtechsltn/SimpleUnitTestingExample.git
git status
git branch
git add .
git commit -m "Init source"
git push --set-upstream origin master
git branch --set-upstream-to=origin/master
git pull --allow-unrelated-histories
git pull origin master
git push
```

Unit Testing using xUnit

https://www.nikolatech.net/blogs/unit-testing-using-xunit-dotnet

Bogus - Generate Realistic Fake Data in C#

https://www.nikolatech.net/blogs/bogus-ganerate-fake-data

Fluent Assertions in Unit Testing in C#

https://www.youtube.com/watch?v=TytferBCLOo

# .NET provides several robust frameworks for unit testing:

* xUnit
* NUnit
* MSTest
* TUnit

# Design Patterns
* AAA (Arrange-Act-Assert)

You may also notice the comments in the test. This follows the AAA (Arrange-Act-Assert) pattern, which is a widely used convention for structuring unit tests:

* Arrange: Set up the necessary objects and state. In this case, we create a new product, which is unpublished by default.
* Act: Perform the action being tested. Here, we call Publish() on the product.
* Assert: Verify the expected outcome. We check if the product’s status is now Published.
* This structure makes tests easier to read and understand while maintaining a clear separation of concerns.

## Handling Exceptions

```
// Act
var exception = Assert.Throws<InvalidOperationException>(() => product.Publish());
```

# Libraries
* Moq
* NSubstitute
* Bogus
* FluentAssertions
