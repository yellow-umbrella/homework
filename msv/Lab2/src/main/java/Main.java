import java.io.*;
import java.util.*;

public class Main {
    public static void main(String[] args) {
        if (args.length < 1) {
            System.out.println("Filename not specified.");
            return;
        }
        try {
            writeResult(processFile(args[0]));
        } catch (Exception exception) {
            System.out.println(exception.getMessage());
        }
    }

    public static List<String> processFile(String file) throws FileNotFoundException {
        Set<String> words = WordProcessor.readWords(file);
        ArrayList<String> res = new ArrayList<>(words);
        Collections.sort(res);
        return res;
    }

    public static void writeResult(List<String> res) throws IOException {
        FileWriter myWriter = new FileWriter("out.txt");
        for (String word: res) {
            myWriter.write(word + "\n");
        }
        myWriter.close();
    }
}
