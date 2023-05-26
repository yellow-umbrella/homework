import java.io.*;
import java.util.*;

public class MyScanner {
    public List<String> readWords(String filename) throws FileNotFoundException{
        List<String> res = new ArrayList<>();
        Scanner scanner = new Scanner(new File(filename));
        while (scanner.hasNext()) {
            String line = scanner.nextLine();
            res.add(line);
        }
        scanner.close();
        return res;
    }
}
