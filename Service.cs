using System;
using System.Threading;
using Hangfire;

public static class Service
{
  public static void DelaySuccess()
  {
    Console.WriteLine("Etapa 1");
    Thread.Sleep(5000);
    Console.WriteLine("Etapa 2");
    Thread.Sleep(2000);
    Console.WriteLine("Ultima Etapa");
  }
  [AutomaticRetry(Attempts = 3)]
  public static void DelayFail()
  {
    try
    {
      Console.WriteLine("Etapa 1");
      Thread.Sleep(1000);
      Console.WriteLine("Etapa 2");
      Thread.Sleep(2000);
      Convert.ToInt32("Erro de conversao de inteiro");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      throw;
    }

  }
  public static void DailySuccess()
  {
    RecurringJob.AddOrUpdate(
    () => Console.WriteLine("Diário recorrente!"),
    Cron.Daily);
  }
  public static void Delayed()
  {
    Console.WriteLine("Delayed!");
  }
}