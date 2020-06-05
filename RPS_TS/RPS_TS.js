"use strict";
//console.log("This is the RPS typescrypt file");
function assignChoiceWord(choice) {
    if (choice == 0) {
        return "rock";
    }
    else if (choice == 1) {
        return "paper";
    }
    else {
        return "scissors";
    }
}
function decideWinner(p1, p2) {
    if (p1.choiceWord == "rock" && p2.choiceWord == "scissors") {
        return p1.name;
    }
    else if (p1.choiceWord == "rock" && p2.choiceWord == "paper") {
        return p2.name;
    }
    else if (p1.choiceWord == "paper" && p2.choiceWord == "rock") {
        return p1.name;
    }
    else if (p1.choiceWord == "paper" && p2.choiceWord == "scissors") {
        return p2.name;
    }
    else if (p1.choiceWord == "scissors" && p2.choiceWord == "paper") {
        return p1.name;
    }
    else if (p1.choiceWord == "scissors" && p2.choiceWord == "rock") {
        return p2.name;
    }
    else {
        return "tie";
    }
}
function randomNum() {
    return Math.floor(Math.random() * 3);
}
var p1 = {
    name: "myself",
    choice: 0,
    choiceWord: "null"
};
var p2 = {
    name: "myotherself",
    choice: 0,
    choiceWord: "null"
};
p1.choice = randomNum();
p2.choice = randomNum();
p1.choiceWord = assignChoiceWord(p1.choice);
p2.choiceWord = assignChoiceWord(p2.choice);
var result = decideWinner(p1, p2);
console.log(result);
