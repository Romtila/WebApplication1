using System;
using System.Linq;

namespace WebApplication1 // как зарегать generic, зарегать тот Baserepository, 
{
    public class UserService
    {
        private readonly BaseRepository<User> _baseRepository;

        public Guid guid;

        public UserService(BaseRepository<User> userRepository)
        {
            _baseRepository = userRepository;

            guid = Guid.NewGuid();
        }

        public void CreateUser(UserView userView)
        {
            ValidateUsername(userView.Username);

            ValidatePassword(userView.Password);

            var user = new User() { Id = userView.Id, 
                Username = userView.Username, 
                Password = userView.Password, 
                FirstName = userView.FirstName, 
                LastName = userView.LastName, 
                Email = userView.Email, 
                BirthDay = userView.BirthDay,};

            _baseRepository.Create(user);
        }

        public User VerificationUser(string username, string password) // в этом методе я бы сделал не void, а возвращал Userы
        {
            var user = _baseRepository.GetMany().FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user != null)
            {
                return user;
            }
            throw new Exception("Username is not or password is wrong");
        }

        public User AuthUser(string username, string password)
        {
            return _baseRepository.FindOne(x => x.Username == username && x.Password == password);
        }

        private static void ValidateUsername(string username)//проверка username
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("Username is empty");

            if (username.Length < 5) throw new Exception("Length of username < 5");
        }

        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new Exception("Password is empty");

            if (password.Length < 6) throw new Exception("Length of password < 6");

            if (password.Any(char.IsUpper) &&
                password.Any(char.IsLower) &&
                password.Any(char.IsNumber))
                throw new Exception("Password должен иметь хотя бы один символ верхнего и маленького регистра и цифру");
        }
    }
}
