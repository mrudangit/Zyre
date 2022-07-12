// See https://aka.ms/new-console-template for more information

using NetMQ.Zyre;
using NetMQ.Zyre.ZyreEvents;

static class Program
{

    [STAThread]
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var zyre = new Zyre("MMDotNet", true);
        
        zyre.Start();
        zyre.Join("CHAT");
        
        zyre.EnterEvent += ZyreOnEnterEvent;
        zyre.LeaveEvent += ZyreOnLeaveEvent;
        zyre.JoinEvent += ZyreOnJoinEvent;
        zyre.StopEvent += ZyreOnStopEvent;
        zyre.ExitEvent += ZyreOnExitEvent;
        zyre.WhisperEvent += ZyreWhisperEvent;
        zyre.ShoutEvent += ZyreShoutEvent;

    
        
    }

    private static void ZyreShoutEvent(object? sender, ZyreEventShout e)
    {

        var content = e.Content;
        var count = content.FrameCount;
        for (int i = 0; i < count; i++)
        {
            var frame = content[i];
            Console.WriteLine($"Shout From : {e.SenderName} Message : {frame.ConvertToString()}");
        }

    }

    private static void ZyreWhisperEvent(object? sender, ZyreEventWhisper e)
    {
        Console.WriteLine($"Whisper Event : From : {e.SenderName} Data : {e.Content.ToString()}");
    }

    private static void ZyreOnExitEvent(object? sender, ZyreEventExit e)
    {
        Console.WriteLine($"Peer Exited : {e.SenderName}");
    }

    private static void ZyreOnStopEvent(object? sender, ZyreEventStop e)
    {
        Console.WriteLine($"Peer Stopped : {e.SenderName}");
    }

    private static void ZyreOnJoinEvent(object? sender, ZyreEventJoin e)
    {
        Console.WriteLine($" Joined : {e.SenderName}");
    }

    private static void ZyreOnLeaveEvent(object? sender, ZyreEventLeave e)
    {
        Console.WriteLine($"Peer Left : {e.SenderName}");
    }

    private static void ZyreOnEnterEvent(object? sender, ZyreEventEnter e)
    {
        Console.WriteLine($"Peer Entered : {e.SenderName}");
    }
}