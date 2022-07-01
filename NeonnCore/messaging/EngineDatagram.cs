using MagicMessagebus.Contract;

namespace NeonnCore.messaging;

public struct EngineDatagram : IMagicMessage
{
    //Uri of the sender
    public string Sender;
    //Uri of the addressee
    public string Recipient;
    public Operation Operation;
    public string Topic;
    public object Data;
}