import 'package:flutter/material.dart';
import 'quizBrain.dart';

void main() => runApp(Quizzler());

var quizBrain = QuizBrain();

class Quizzler extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        backgroundColor: Colors.grey.shade900,
        body: SafeArea(
          child: Padding(
            padding: EdgeInsets.symmetric(horizontal: 10.0),
            child: QuizPage(),
          ),
        ),
      ),
    );
  }
}

class QuizPage extends StatefulWidget {
  @override
  _QuizPageState createState() => _QuizPageState();
}

class _QuizPageState extends State<QuizPage> {
  List<Icon> scoreKeeper = [];
  List<MaterialColor> colors = [];

  MaterialColor randomColor() {
    if (quizBrain.questionAnswer()) {
      if (quizBrain.isCorrect()) return Colors.green;
      else return Colors.red;
    }
    if (colors.length == 0) {
      colors = [Colors.red, Colors.green, Colors.purple, Colors.teal, Colors.blueGrey, Colors.brown, Colors.deepOrange, Colors.indigo];
    }  
    colors.shuffle();
    var returnColor = colors.first;
    colors.removeAt(0);
    return returnColor;
  }

  List<Expanded> answers() {
    var answers = quizBrain.getAnswers();
    var expandedAnswers = <Expanded>[];


    answers.forEach((answer) {
      expandedAnswers.add(
        Expanded(
          child: Padding(
            padding: EdgeInsets.all(15.0),
            child: FlatButton(
              textColor: Colors.white,
              color: randomColor(),
              child: Text(
                answer,
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 20.0,
                ),
              ),
              onPressed: () {
                // the user picked true
                setState(() {
                  quizBrain.answer(answer);
                  quizBrain.nextQuestion();
                  //else if (quizBrain.isCorrect(answer)) scoreKeeper.add(Icon(Icons.check, color: Colors.green));
                  //else scoreKeeper.add(Icon(Icons.close, color: Colors.red));
                  //quizBrain.nextQuestion();                
                });
              },
            ),
          ),
        )
      );
    });
    return expandedAnswers;
  }

  @override
  Widget build(BuildContext context) {
    var items = <Widget>[];
    items.add(Expanded(
          flex: 5,
          child: Padding(
            padding: EdgeInsets.all(10.0),
            child: Center(
              child: Text(
                quizBrain.getQuestion(),
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 25.0,
                  color: Colors.white,
                ),
              ),
            ),
          ),
        ));
    items.addAll(answers());
    items.add(Row(children: scoreKeeper));
    
    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: items
    );
  }
}
