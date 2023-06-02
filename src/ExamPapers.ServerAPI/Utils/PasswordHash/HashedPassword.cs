namespace ExamPapers.ServerAPI.Utils.PasswordHash;

public record HashedPassword(byte[] Salt, byte[] Hash)
{
    public const int HASH_SIZE = 64;
    public const int SALT_SIZE = 8;
    public const int COUNT_ITRTATIONS = 1000;

    public string Hex
    {
        get
        {
            var saltHash = new byte[SALT_SIZE + HASH_SIZE];
            
            Buffer.BlockCopy(Salt, 0, saltHash, 0, SALT_SIZE);
            Buffer.BlockCopy(Hash, 0, saltHash, SALT_SIZE, HASH_SIZE);

            return Convert.ToHexString(saltHash);
        }
    }

    public static HashedPassword FromHexString(string hexHash)
    {
        ArgumentException.ThrowIfNullOrEmpty(hexHash);
        
        var saltHash = Convert.FromHexString(hexHash);

        var salt = new byte[SALT_SIZE];
        var hash = new byte[HASH_SIZE];
        
        Buffer.BlockCopy(saltHash, 0, salt, 0, SALT_SIZE);
        Buffer.BlockCopy(saltHash, SALT_SIZE, hash, 0, HASH_SIZE);

        return new HashedPassword(salt, hash);
    }

    public virtual bool Equals(HashedPassword? other)
    {
        if (other == null)
            return false;

        return Salt.SequenceEqual(other.Salt) && Hash.SequenceEqual(other.Hash);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Salt, Hash);
    }
}
