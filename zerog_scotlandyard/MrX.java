package zerog_scotlandyard;
import static zerog_scotlandyard.TransportType.*;

public class MrX extends Player {
	public MrX(String name, int id){
		super(name, id);
		tickets.put(bus, 4);
		tickets.put(taxi, 3);
		tickets.put(underground, 3);
		tickets.put(blackCard, 2);
	}
}