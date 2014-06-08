package zerog_scotlandyard;
import static zerog_scotlandyard.TransportType.*;

public class MrX extends Player {
	public MrX(String name, int id){
		super(name, id);
		tickets.put(taxi, 4);
		tickets.put(bus, 3);
		tickets.put(underground, 3);
		tickets.put(blackCard, 2);
	}
}