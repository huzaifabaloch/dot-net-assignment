using Problem2;
using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetAssignment
{
    class Program
    {

        static List<Branch> GetFilesForFolder(Branch branch)
        {
            // If a folder has files 
            var files = Directory.GetFiles(branch.path);
            List<Branch> childs = new List<Branch>();
            foreach (var file in files)
            {
                childs.Add(new Branch(file));
            }
            return childs;
        }

        /// <summary>
        /// This method will create a hierarical structure with recurison.
        /// Initially the 'path' will hold the root folder path.
        /// 'head' will be tracking the state of the object 'Branch'.
        /// It will keep on creating the hierarchy until it reaches to the end.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="head"></param>
        /// <returns></returns>
        static Branch CreateHierarchyWithRecursion(string path, Branch head)
        {
            Branch branch = new Branch(path);
            branch.children = GetFilesForFolder(branch);

            // Initially head will be null so we assign entire branch to head.
            if (head == null)
                head = branch;
            else
            {
                int counter = 0; // to track the state of object.

                /* 
                 * getting all the children of head branch.
                   this obj will serve as adding new branch objects to specific position.
                */
                List<Branch> obj = head.children;
                while (true)
                {
                    /*
                     * After getting all children, we need to locate the correct position to put the object;
                     * the if section checks for files, since files don't have children we just increment counter,
                     * and get closer to folder section, which will take to the else part.
                     */
                    for (int i = 0; i < obj.Count; i++)
                    {
                        if (obj[i].children == null)
                            counter++;
                        else
                        {
                            /*
                             * here we will get the children for the folder if it contains,
                             * here we have 4 if statements, all have purpose
                             */
                             
                            obj = obj[i].children;
                            if (obj.Count > 1 && obj[obj.Count - 1].children == null)
                            {
                                counter = obj.Count;
                                break;
                            }

                            /* checks if the folder has more than one child, then
                             * maybe we have some of them files object. we then get that entire obj
                             * and re assign obj, reset i and counter and restart the loop.
                             */
                            if (obj.Count > 1)
                            {
                                obj = obj[obj.Count - 1].children;
                                i = -1;
                                counter = 0;
                                continue;
                            }

                            /*
                             * This check is simple, if obj count is zero, we just 
                             * set obj count to counter to exit from loop and add the branch obj to head.
                             */
                            if (obj.Count == 0)
                            {
                                counter = obj.Count;
                                break;
                            }

                            /*
                             * This check is for object which only has a folder object, but no file object. 
                             */
                            if (obj[0].children != null)
                            {
                                obj = obj[obj.Count - 1].children;
                                i = -1;
                                counter = 0;
                                continue;
                            }
                            counter = obj.Count;
                        }
                    }

                    /* This condition will only add the new object into correct position into object hierarchy
                        based on above conditions.
                     */
                    if (counter == obj.Count)
                    {
                        obj.Add(branch);
                        break;
                    }
                }
            }

            // Similarly we get the sub folder and pass that into same method.
            var folders = Directory.GetDirectories(branch.path);
            foreach (var folder in folders)
            {
                return CreateHierarchyWithRecursion(folder, head);
            }

            return head;
        }

        /// <summary>
        /// This method will calculate the depth of the created hierarical structure.
        /// </summary>
        /// <param name="head"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        static int FindDepth(Branch head, int depth)
        {
            while (head.path != string.Empty && head.children != null)
            {
                for (int i = 0; i < head.children.Count; i++)
                {
                    if (head.children[i].children != null)
                    {
                        depth++;
                        head = head.children[i];
                        return FindDepth(head, depth);
                    }
                }
                if (head.children.Count == 0)
                    break;
                if (head.children[head.children.Count - 1].children == null)
                {
                    depth++;
                    break;
                }
            }
            return depth;
        }

        /// <summary>
        /// This method will display the object hirarchy
        /// </summary>
        /// <param name="head"></param>
        static void ShowObjectHierachy(Branch head)
        {
            int spaces = 0;
            int depth = 1;
            bool lastObj = false;

            int objCount = head.children.Count;

            if (!string.IsNullOrEmpty(head.path))
            {
                if (!CheckIfOneOnlyObject(head))
                {
                    while (objCount > 0 && !lastObj)
                    {
                        var headPathSplit = head.path.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + headPathSplit[headPathSplit.Length - 1] + ' ' + '(' + depth.ToString() + ')');
                        spaces += 5;
                        // get, if the obj has children
                        var children = head.children;
                        for (int i = 0; i < children.Count; i++)
                        {
                            if (children[i].children == null)
                            {
                                string[] pathSplitForFile = head.children[i].path.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + pathSplitForFile[pathSplitForFile.Length - 1]);
                                objCount--;
                            }
                            else
                            {
                                head = children[i];
                                objCount = head.children.Count;
                                depth++;
                            }
                        }

                        // EXCEPTIONAL CASE FOR LAST OBJECT
                        // --------------------------------
                        // Exceptional case for LAST folder with no files in it.
                        var files = Directory.GetFiles(head.path);
                        if (files.Length == 0 && head.children.Count == 0)
                        {
                            headPathSplit = head.path.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + headPathSplit[headPathSplit.Length - 1] + ' ' + '(' + depth.ToString() + ')');
                            spaces += 5;
                            lastObj = true;
                        }
                        // Exceptional case for LAST folder with only files in it.
                        var folders = Directory.GetDirectories(head.path);
                        if (folders.Length == 0 && head.children.Count > 0)
                        {
                            headPathSplit = head.path.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + headPathSplit[headPathSplit.Length - 1] + ' ' + '(' + depth.ToString() + ')');
                            depth++;
                            spaces += 5;
                            for (int i = 0; i < head.children.Count; i++)
                            {
                                Branch child = head.children[i];
                                string[] pathSplitForFile = child.path.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                if (i != (head.children.Count - 1))
                                    Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + pathSplitForFile[pathSplitForFile.Length - 1]);
                                else
                                    Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + pathSplitForFile[pathSplitForFile.Length - 1] + ' ' + '(' + depth.ToString() + ')');
                            }
                            lastObj = true;
                        }
                    }
                }
                else
                {
                    var headPathSplit = head.path.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + headPathSplit[headPathSplit.Length - 1] + ' ' + '(' + depth.ToString() + ')');
                    depth++;
                    spaces += 5;
                    for (int i = 0; i < head.children.Count; i++)
                    {
                        Branch child = head.children[i];
                        string[] pathSplitForFile = child.path.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (i != (head.children.Count - 1))
                            Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + pathSplitForFile[pathSplitForFile.Length - 1]);
                        else
                            Console.WriteLine(string.Empty.PadLeft(spaces) + "-> " + pathSplitForFile[pathSplitForFile.Length - 1] + ' ' + '(' + depth.ToString() + ')');
                    }
                }
            }
        }


        static bool CheckIfOneOnlyObject(Branch head)
        {
            foreach (var child in head.children)
            {
                if (child.children != null)
                    return false;
            }
            return true;
        }


        static void Main(string[] args)
        {
            string folderName = "RootFolder";
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", folderName);

            // branch object
            Branch branchHead = null;

            // Creating object hierarchy using recursion.
            Branch branchHierarchy = CreateHierarchyWithRecursion(path, branchHead);

            // Calculating object depth.
            int depth = 1;
            depth = FindDepth(branchHierarchy, depth);

            // Displaying object hierarchy.
            ShowObjectHierachy(branchHierarchy);
            Console.WriteLine();

            // Printing the depth of the object.
            Console.WriteLine($"The depth of this structure is {depth}");

            Console.ReadLine();
        }
    }
}
