using System.Security.Cryptography;
using System.Text;

namespace ExamPapers.API.Server.Utils.PasswordHash;

public class PasswordHasher : IPasswordHasher
{
    public HashedPassword GenerateHashPassword(string password)
    {
        using var rfc2898 = new Rfc2898DeriveBytes(
            password: password,
            saltSize: HashedPassword.SALT_SIZE,
            iterations: HashedPassword.COUNT_ITRTATIONS,
            hashAlgorithm: HashAlgorithmName.SHA512);

        return new HashedPassword(rfc2898.Salt, rfc2898.GetBytes(HashedPassword.HASH_SIZE));
    }

    public bool ValidatePassword(string password, string hashPassword)
    {
        var validHash = HashedPassword.FromHexString(hashPassword);

        using var rfc2898 = new Rfc2898DeriveBytes(
            password: password,
            salt: validHash.Salt,
            iterations: HashedPassword.COUNT_ITRTATIONS,
            hashAlgorithm: HashAlgorithmName.SHA512);
        var hash = new HashedPassword(rfc2898.Salt, rfc2898.GetBytes(HashedPassword.HASH_SIZE));

        return validHash == hash;
    }
}