﻿using DealFinder.Core.Communication;
using DealFinder.Core.Security;
using DealFinder.Data.ThirdPartyAuthenticator;
using DealFinder.Data.Users.Repository;
using DealFinder.Data.Users.Service;
using Moq;
using NUnit.Framework;

namespace DealFinder.Data.Tests.Users.Service.GivenARequestToRegisterAUser
{
    [TestFixture]
    public class WhenUserRepositoryReturnsAnErrorWhenSavingTheUSer
    {
        private RegisterResponse _result;
        private Mock<IAuthenticatorFactory> _authenticationFactory;
        private Mock<IAuthenticator> _authenticator;
        private Mock<IUserRepository> _userRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.CreateUser(It.IsAny<CreateUserRequest>())).Returns(new CreateUserResponse());
            _userRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(new GetUserResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.AuthenticatorError,
                    UserMessage = "Some User Message",
                    TechnicalMessage = "Some Technical Message"
                }
            });

            _authenticator = new Mock<IAuthenticator>();
            _authenticator.Setup(x => x.Authenticate(It.IsAny<IAuthenticationRequest>())).Returns(new BaseAuthenticationResponse
            {
                Username = "SOME_USERNAME",
                Picture = "SOME_PICTURE",
                UserId = "SOME_USER_ID"
            });

            _authenticationFactory = new Mock<IAuthenticatorFactory>();
            _authenticationFactory.Setup(x => x.For(It.IsAny<Authenticator>())).Returns(_authenticator.Object);
;
            var keyReader = new Mock<IKeyReader>();
            keyReader.Setup(x => x.GetKey()).Returns("SOME_KEY");

            var subject = new UserService(_userRepository.Object, _authenticationFactory.Object, new UserMapper(new AesEncryptor(keyReader.Object.GetKey())));
            _result = subject.Register(new RegisterRequest
            {
                UserToken = "SOME_TOKEN",
                Authenticator = Authenticator.Google
            });
        }

        [Test]
        public void ThenAuthenticatorFactoryIsCalledWithCorrectlyMappedAuthenticator()
        {
            _authenticationFactory.Verify(x => x.For(It.Is<Authenticator>(y => y == Authenticator.Google)));
        }

        [Test]
        public void ThenUserTokenIsMappedCorrectlyOntoTheAuthenticator()
        {
            _authenticator.Verify(x => x.Authenticate(It.Is<IAuthenticationRequest>(y => y.Token == "SOME_TOKEN")));
        }

        [Test]
        public void ThenUserRepositoryIsCalledWithCorrectlyMappedUsername()
        {
            _userRepository.Verify(x => x.CreateUser(It.Is<CreateUserRequest>(y => y.User.Username == "SOME_USERNAME")));
        }

        [Test]
        public void ThenUserRepositoryIsCalledWithCorrectlyMappedPicture()
        {
            _userRepository.Verify(x => x.CreateUser(It.Is<CreateUserRequest>(y => y.User.Picture == "SOME_PICTURE")));
        }

        [Test]
        public void ThenUserRepositoryIsCalledWithCorrectlyMappedUserToken()
        {
            _userRepository.Verify(x => x.CreateUser(It.Is<CreateUserRequest>(y => y.User.UserToken == "SOME_USER_ID")));
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.AuthenticatorError));
        }

        [Test]
        public void ThenAnUserErrorMessageCodeIsReturned()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Some User Message"));
        }

        [Test]
        public void ThenAnTechnicalErrorMessageCodeIsReturned()
        {
            Assert.That(_result.Error.TechnicalMessage, Is.EqualTo("Some Technical Message"));
        }
    }
}