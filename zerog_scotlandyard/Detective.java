package zerog_scotlandyard;
import java.util.*;
import static zerog_scotlandyard.TransportType.*;

public class Detective extends Player {
	public Detective(String name, int id){
		super(name, id);
		tickets.put(taxi, 10);
		tickets.put(bus, 8);
		tickets.put(underground, 4);
	}
}