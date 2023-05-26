package Lab1;
// Нємкевич Дар'я, Лабораторна 1, Варiант 5
// Знайти лише ті слова, які мають здвоєні приголосні літери.

import java.io.*;
import java.util.*;

public class Lab1 {
    static final String consonants = "qwrtpsdfghklzxcvbnm";
    static final int maxLength = 30;

    public static void main(String[] args) {
        if (args.length == 0) {
            System.out.println("Filename not specified.");
            return;
        }

        Set<String> words = readWords(args[0]);

        if (words != null) {
            List<String> res = new ArrayList<String>(words);
            Collections.sort(res);
            try {
                FileWriter myWriter = new FileWriter("out.txt");
                for (String word: res) {
                    myWriter.write(word + "\n");
                }
                myWriter.close();
            } catch (IOException e) {
                System.out.println("An error with writing to file occurred.");
                res.forEach(System.out::println);
            }
        }
    }

    public static Set<String> readWords(String filename) {
        Set<String> words = new HashSet<String>();
        Scanner scanner;

        try {
            scanner = new Scanner(new FileReader(filename));
        } catch (FileNotFoundException exception) {
            System.out.println("File not found.");
            return null;
        }

        while (scanner.hasNext()) {
            String line = scanner.nextLine();
            for (String word: line.split("\\W+")) {
                if (word.length() > maxLength) {
                    word = word.substring(0, maxLength);
                }
                if (hasDoubledConsonants(word)) {
                    words.add(word);
                } 
            }
        }

        scanner.close();
        return words;
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