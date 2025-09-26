namespace AntojosSV.Endpoints
{
    public static class Startup
    {
        public static void UsarEndpoints(this WebApplication app)
        {
            EncargosEndpoints.Add(app);
            ComidasEndpoints.Add(app);
            UsuarioEndpoints.Add(app);
            CategoriaEndpoints.Add(app);
            //

        }
    }
}
