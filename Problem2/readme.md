## Problem 2

Write a program, where you would create similar structure. Pass this structure into your own 
created method and calculate the depth of provided structure. Main requirement to complete this 
task: use recursion.


I divided this problem into 3 parts:
1. Creating a hierarical class model.
2. Intiailizing that hierarical class using recursion.
3. Calculating the depth of the hierarical structure using recursion.


### Problem Solution
### =================
### I take the example of a file directory where you could have nested folder structure.
### Example
### =======

     ->	RootFolder  (1)           
	         -> rootFile1.txt 
             -> rootFile2.txt
             -> Folder1  (2)
		          -> folder1File.txt
		          -> Folder2  (3)
			           -> folder2File.txt
			           -> Folder3  (4)
				            -> Folder4  (5)
					             -> folder4file.txt
					             -> Folder5  (6)

	 The depth of this structure is 6	


### This folder structure is added in the Problem2 folder, namely RootFolder.


## Note
## ====
### One thing to note here that, I was supposed to create a similar structure, so I tried my best to create similar one to the problem. So It may not work properly for folder having more than one folder :) but for more than one file, it will work fine..
### Example
### =======

      ->RootFolder
	         -> rootFile1.txt
                 -> rootFile2.txt
                 -> Folder1    // Root folder have 2 folders (Folder1 and Folder2) (not work)
		 -> Folder2    // Root folder have 2 folders (Folder1 and Folder2) (not work)
							


