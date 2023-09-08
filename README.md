# FoodicsTask
This is Lina Albaroudi's attempt on solving Foodics technical task for a Quality Assurance Engineer role. 

## Task A Scope
1. Open https://www.google.com and search for any keyword
2. Remove the keyword and search for a new one
3. Assert that number of results exist on UI
4. Scroll down and go to the next page
5. Validate if the number of results on page 2 is equal to page 3 or not
6. Validate there are different search suggestions displayed at the end of the page
7. Close the browser window

## Requirements
1. Design framework using **page object model**.
2. Make sure **functions are reusable**.
3. Make sure you write code using proper **coding standards**.
4. Add a **readme file** which should be clear enough and the evaluator can easily
5. **install the project with all dependencies** and run it successfully.

## Notes to The Reviewer
I have choosen to write the code in C# because it is what I am mostly experienced in regarding Selenium tests. 
In Zone, my current company, all code base is written in C#, so I had to comply to the company's rules.
However, I have also developed selenium tests using Java and Python, but not in a real project level.   

Sometimes the test fails in finding the "NextButton" element because of two reasons:
1. The next **NextButton** element is not always displayed.   
2. Or because the google sign in window is overlapping with the **NextButton** element.   
The test results with screenshots will be added to **FoodicsTask\GoogleTest\index.html** file

## Installation steps
1. 
2. Open Microsoft Visual Studio
3. Select **Clone Repository**
4. In Repository Location Field, paste the copied link.
5. 
