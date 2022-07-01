namespace NeonnCore.actor;
using NeonnCore.messaging;
public interface IActor
{
    
    public void SendMessage();
    //Uses SendMessage to send a datagram
    public void Create(EngineDatagram datagram);
    // Reads a datagram from the Monolith static method 'public static void Subscribe(EngineDatagram datagram)'
    public void Read(EngineDatagram datagram);
    // Uses SendMessage to send a datagram
    public void Update(EngineDatagram datagram);
    // Uses SendMessage to send a datagram
    public void Delete(EngineDatagram datagram);
    
}