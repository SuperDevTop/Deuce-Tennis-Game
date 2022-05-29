using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Telegram : IComparable<Telegram>
{
	int sender;
	int receiver;
	
	int msg;
	
	long dispatchTime;
	
	Object extraInfo;
	public Telegram( long delay, int sender, int receiver, int msg, Object extra)
	{
		dispatchTime = delay;
		this.sender = sender;
		this.receiver = receiver;
		this.msg = msg;
		this.extraInfo = extra;
	}

    public int CompareTo(Telegram o)
	{
		long compareTime = o.dispatchTime;
		
		if( this.dispatchTime - compareTime > 0  )
			return 1;
		else if( this.dispatchTime - compareTime == 0  )
			return 0;
		else
			return -1;
	}
	public override String ToString()
	{
		String s = "Delay; " + dispatchTime 
				+ ", sender: " + sender
				+ ", receiver: " + receiver
				+ ", msg: " + msg
				+ ", Object; " + ((extraInfo == null)?"null":extraInfo.ToString());
		return s;
	}
}
