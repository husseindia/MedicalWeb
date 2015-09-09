using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace MedicalWeb.Models
{
    public class Pharmci
    {
        public virtual Guid Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual string ZoneName { get; set; }

        public virtual int BloodType { get; set; }

        public string Phone { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = true)]
        public virtual string DateOfBirth { get; set; }

        //[DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)
        [Required(AllowEmptyStrings = true)]
        public virtual string LastDonationDate { get; set; }

        //[DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = true)]
        public virtual string RegistrationDate { get; set; }

        public virtual bool IsFirstTimeDonor { get; set; }

        public virtual void SetPassword(string passwordText)
        {
            PasswordSalt = Guid.NewGuid().ToString();
            PasswordHash = Hash(passwordText, PasswordSalt);
        }

        public virtual string Hash(string value, string salt)
        {
            var hashed = this.Hash(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(salt));
            return Convert.ToBase64String(hashed);
        }

        public virtual byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();
            return new SHA256Managed().ComputeHash(saltedValue);
        }

        public virtual bool VerifyPassword(string password)
        {
            var hashpassword = Hash(password, PasswordSalt);
            return PasswordHash.SequenceEqual(hashpassword);
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }


    public class PharmciRepositories : MedicalContext
    {
    private MedicalContext db = new MedicalContext();
    public void CreateUser(Pharmci donor)
    {

        if (GetByUserName(donor.UserName) != null)
            throw new Exception("A user with same username already exists");

        // Set Creation Date
        donor.RegistrationDate = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        donor.DateOfBirth = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        donor.LastDonationDate = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        donor.IsFirstTimeDonor = true;

        db.Pharmcis.Add(donor);

        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {

        }

    }

    public Pharmci GetByUserName(string userName)
    {
        var donor = from s in db.Pharmcis where s.UserName.Equals(userName) select s;
        int count = donor.Count();

        if (count == 0)
        {
            return null;
        }
        Pharmci returnDonor = donor.First();
        return returnDonor;

    }

    public Pharmci GetByUserId(Guid donorId)
    {
        var donor = from s in db.Pharmcis where s.Id.Equals(donorId) select s;
        int count = donor.Count();

        if (count == 0)
        {
            return null;
        }
        Pharmci returnDonor = donor.First();
        return returnDonor;

    }

    public void Method()
    {
        throw new System.NotImplementedException();
    }
    }


}
