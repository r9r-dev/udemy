import 'package:quizzler/question.dart';

class QuizBrain {
  List<Question> _questions = [
    Question(
        question: 'En quelle année est sorti le premier iPhone ?',
        answers: ['2003', '2005', '2007'],
        correctAnswer: '2007',
        flavor:
            'L\'iPod est toujours aujourd\'hui le balladeur numérique le plus vendu au monde (2019).'),
    Question(
        question: 'Quel est le nom du héros de The Elder Scrolls V : Skyrim ?',
        answers: ['Dovahkiin', 'Tamriel', 'Harkon'],
        correctAnswer: 'Dovahkiin',
        flavor:
            'Les jeux de la série The Elders Scrolls sont des jeux indépendants développés et édités par Bethesda.'),
  ];
  int _currentQuestion = 0;
  bool _answer = false;
  bool _correct = false;
  String _flavor;

  QuizBrain() {
    _questions.shuffle();
  }

  questionAnswer() => _answer;

  nextQuestion() {
    if (_answer && _currentQuestion < _questions.length - 1) _currentQuestion++;
    _flavor = _questions[_currentQuestion].flavor;
    _answer = !_answer;
  }

  bool isCorrect() => _correct;

  String getQuestion() {
    if (_answer) {
      String correct =
          _correct ? 'Bonne réponse !\n\n' : 'Mauvaise réponse.\n\n';
      return correct + _flavor;
    } else
      return _questions[_currentQuestion].question;
  }

  List<String> getAnswers() {
    if (_answer)
      return ['Question suivante'];
    else
      return _questions[_currentQuestion].answers..shuffle();
  }

  //String getSuccessText() => _questions[_currentQuestion].flavor;

  answer(String answer) {
    //_answer = true;
    _correct = _questions[_currentQuestion].correctAnswer == answer;
  }
}
