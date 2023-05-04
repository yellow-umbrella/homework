package com.manty.lab3;

import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Set;

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

    public static List<String> processFile(String file) throws IOException {
        WordProcessor wordProcessor = new WordProcessor();
        Set<String> words = wordProcessor.chooseWords(file);
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
