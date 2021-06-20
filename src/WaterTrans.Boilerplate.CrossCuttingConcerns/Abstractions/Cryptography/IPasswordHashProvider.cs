namespace WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.Cryptography
{
    public interface IPasswordHashProvider
    {
        byte[] Hash(string password, byte[] salt, int iterations);
        bool Verify(string password, byte[] salt, int iterations, byte[] hashedPassword);
    }
}
