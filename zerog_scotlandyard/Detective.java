package zerog_scotlandyard;
import java.util.*;
import static zerog_scotlandyard.TransportType.*;

public class Detective extends Player {
	public Detective(String name, int id){
		super(name, id);
		tickets.put(bus, 10);
		tickets.put(taxi, 8);
		tickets.put(underground, 4);
	}
}