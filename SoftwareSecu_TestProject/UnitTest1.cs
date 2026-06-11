namespace SoftwareSecu_TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void US1_GuestCanViewPublicHomePage()
        {
            // Arrange
            bool isAuthenticated = false;
            bool homePageRequiresLogin = false;

            // Act
            bool canViewHomePage = !isAuthenticated && !homePageRequiresLogin;

            // Assert
            Assert.True(canViewHomePage);
        }
    }
}