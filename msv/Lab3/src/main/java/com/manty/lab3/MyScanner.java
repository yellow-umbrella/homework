package com.manty.lab3;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class MyScanner {
    public List<String> readLines(String filename) throws FileNotFoundException{
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
