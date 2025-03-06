using ClassLibrary1;
using FluentAssertions;
using FluentAssertions.Execution;

public class SampleClassTests
{
    [Theory]
    [InlineData("Tim Corey")]
    [InlineData("Sue Storm")]
    public void TestFullNameProperty(string fullName)
    {
        //Arrange
        SampleClass sampleClass = new SampleClass(fullName);

        //Act
        string actual = sampleClass.FullName;

        //Assert
        Assert.Equal(fullName, actual);
    }

    [Theory]
    [InlineData("Tim", "Tim Corey")]
    [InlineData("Sue", "Sue Storm")]
    public void TestFirstNameProperty(string firstName, string fullName)
    {
        //Arrange
        SampleClass sampleClass = new SampleClass(fullName);

        //Act
        string actual = sampleClass.FirstName;

        //Assert
        Assert.Equal(firstName, actual);
    }

    [Theory]
    [InlineData("Corey", "Tim Corey")]
    [InlineData("Storm", "Sue Storm")]
    public void TestLastNameProperty(string lastName, string fullName)
    {
        //Arrange
        SampleClass sampleClass = new SampleClass(fullName);

        //Act
        string actual = sampleClass.LastName;

        //Assert
        Assert.Equal(lastName, actual);
    }

    [Theory]
    [InlineData("Eddie", "Van Halen", "Eddie Van Halen")]
    public void TestEdgeCaseNames(string firstName, string lastName, string fullName)
    {
        //Arrange
        SampleClass sampleClass = new SampleClass(fullName);

        using var _ = new AssertionScope();

        sampleClass.LastName.Should().Be(lastName);

        sampleClass.LastName.Should()
            .StartWith(lastName.Substring(0, 3))
            .And.EndWith(lastName.Substring(lastName.Length - 3))
            .And.Contain(" ");
    }
}
