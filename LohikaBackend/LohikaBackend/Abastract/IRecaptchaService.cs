namespace LohikaBackend.Abastract
{
    public interface IRecaptchaService
    {
        /// <summary>
        /// Check is recaptchaToken іs valid
        /// </summary>
        /// <param name="recaptchaToken">Recaptcha token</param>
        /// <returns></returns>
        bool IsValid(string recaptchaToken);
    }
}
