namespace Pingle.UI.Win;

static class Program
{
    internal static IServiceProvider ServiceProvider;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            ServiceProvider = Bootstrapper.Builder.BuildDependencies();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Unrecoverable error encountered during initialization");
            Bootstrapper.Builder.WritePanicLog(ex, "Unrecoverable error encountered during initialization");
            throw;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new BaseWindow());
    }
}
