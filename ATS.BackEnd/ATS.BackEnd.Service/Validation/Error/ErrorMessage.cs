namespace ATS.BackEnd.Service.Validation
{
    public class ErrorMessage
    {
        public ErrorMessage(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; }
    }
}
