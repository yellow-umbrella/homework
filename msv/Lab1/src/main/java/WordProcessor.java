import java.io.*;
import java.util.*;

public class WordProcessor {
    final String consonants = "qwrtpsdfghklzxcvbnm";
    final int maxLength = 30;
    MyScanner scanner = new MyScanner();

    public Set<String> readWords(String filename) throws FileNotFoundException{
        Set<String> words = new HashSet<String>();
        List<String> lines = scanner.readWords(filename);
        for (String line: lines) {
            words.addAll(findWordsInLine(line));
        }
        return words;
    }

    public Set<String> findWordsInLine(String line) {
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

    public boolean hasDoubledConsonants(String word) {
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
