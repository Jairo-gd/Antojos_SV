namespace AntojosSV.Endpoints
{
    public static class Startup
    {
        public static void UsarEndpoints(this WebApplication app)
        {
            EncargosEndpoints.Add(app);
            UsuarioEndpoints.Add(app);
            ComidasEndpoints.Add(app);
            //

        }
    }
}
