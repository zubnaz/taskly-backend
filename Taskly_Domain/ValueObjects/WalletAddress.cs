using Solnet.Wallet;

namespace Taskly_Domain.ValueObjects;

public record WalletAddress(string Value)
{
    /// <summary>
    /// Creates a new WalletAddress instance after validating the public key.
    /// </summary>
    /// <param name="publicKey">The Solana public key as a Base58-encoded string.</param>
    /// <returns>A valid WalletAddress instance.</returns>
    /// <exception cref="ArgumentException">Thrown if the public key is invalid or null.</exception>
    public static WalletAddress Create(string publicKey)
    {
        if (string.IsNullOrWhiteSpace(publicKey))
            throw new ArgumentException("Public key cannot be null or empty.");

        if (!PublicKey.IsValid(publicKey))
            throw new ArgumentException("Invalid Solana public key format.");

        return new WalletAddress(publicKey);
    }

    /// <summary>
    /// Validates a Solana public key string.
    /// </summary>
    /// <param name="publicKey">The Solana public key as a Base58-encoded string.</param>
    /// <returns>
    /// True if the public key is not null or whitespace and has a valid Solana format; otherwise, false.
    /// </returns>
    public static bool ValidatePublicKey(string publicKey)
    {
        return !string.IsNullOrWhiteSpace(publicKey) && PublicKey.IsValid(publicKey);
    }
}