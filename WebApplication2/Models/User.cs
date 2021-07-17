using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace WebApplication2.Models
{
    public class User
    {
        string name;
        string lastName;
        string mail;
        string password;
        string phoneNum;
        string gender;
        int birthY;
        string genre;
        string address;
        int id;
        bool isAdmin;

        public User(string name, string lastName, string mail, string password, string phoneNum, string gender, int birthY, string genre, string address, int id)
        {
            this.name = name;
            this.lastName = lastName;
            this.mail = mail;
            this.password = password;
            this.phoneNum = phoneNum;
            this.gender = gender;
            this.birthY = birthY;
            this.genre = genre;
            this.address = address;
            this.id = id;
        }
        public User()
        {

        }
        public int Insert()
        {
            DataServices ds = new DataServices();
            ds.Insert(this);
            return 1;
        
        }

        public List<User> Get()
        {
            DataServices ds = new DataServices();
            List<User> uList = ds.GetUsers();
            return uList;
        }

        public User Get(string email, string password)
        {
            DataServices ds = new DataServices();
            User user = ds.Get(email, password);
            return user;
        }

        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Gender { get => gender; set => gender = value; }
        public int BirthY { get => birthY; set => birthY = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Address { get => address; set => address = value; }
        public int Id { get => id; set => id = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
    }
}