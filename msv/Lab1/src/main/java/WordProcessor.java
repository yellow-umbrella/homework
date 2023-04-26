import java.io.*;
import java.util.*;

public class WordProcessor {
    static final String consonants = "qwrtpsdfghklzxcvbnm";
    static final int maxLength = 30;

    public static Set<String> readWords(String filename) throws FileNotFoundException{
        Set<String> words = new HashSet<String>();
        Scanner scanner = new Scanner(new File(filename));
        while (scanner.hasNext()) {
            String line = scanner.nextLine();
            words.addAll(findWordsInLine(line));
        }
        scanner.close();
        return words;
    }

    public static Set<String> findWordsInLine(String line) {
        Set<String> res = new HashSet<String>();
        for (String word: line.split("\\W+")) {
            if (word.length() > maxLength) {
                word = word.substring(0, maxLength);
            }
            if (hasDoubledConsonants(word)) {
                res.add(word);
            }
        }
        return res;
    }

    public static boolean hasDoubledConsonants(String word) {
        char prevLetter = ' ';
        for (char letter: word.toLowerCase().toCharArray()) {
            if (prevLetter == letter
                    && consonants.contains("" + letter)) {
                return true;
            }
            prevLetter = letter;
        }
        return false;
    }
}
