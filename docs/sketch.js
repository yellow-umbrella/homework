let graph;
let maxX = 1000-1, maxY = 500-1;

let buttonReset, buttonTriangulate, buttonGenNPoints;
let inputN;
let resultText, nText;

let maxN = 100000;

function setup() {
  createCanvas(maxX+1, maxY+1);
  graph = new Graph();

  buttonReset = createButton('Очистити');
  buttonReset.position(maxX + 10, 90);
  buttonReset.mousePressed(buttonResetClicked);

  buttonTriangulate = createButton('Триангулювати');
  buttonTriangulate.position(maxX + 10, 130);
  buttonTriangulate.mousePressed(buttonTriangulateClicked);

  buttonGenNPoints = createButton('Додати n точок');
  buttonGenNPoints.position(maxX + 10, 170);
  buttonGenNPoints.mousePressed(buttonGenNPointsClicked);

  inputN = createInput();
  inputN.attribute('type', 'number');
  inputN.attribute('min', str(0));
  inputN.attribute('max', str(maxN));
  inputN.attribute('value', '100');
  inputN.position(maxX + 10, 210);
  inputN.input(inputNewN);

  resultText = createP('');
  resultText.position(maxX + 10, 40);
  nText = createP('');
  nText.position(maxX + 10, 10);

  draw_sketch()
}

function draw_sketch() {
  background(0);
  graph.render();
}

function mousePressed() {
  if (mouseX < maxX && mouseY < maxY) {
    graph.points.push(new Point(mouseX, mouseY));
    draw_sketch()
  }
}

function inputNewN() {
  let n = min(max(0, int(inputN.value())), maxN);
  inputN.attribute('value', str(n));
  console.log(inputN.value());
}

function buttonTriangulateClicked() {
  graph.execute();
  draw_sketch();
}

function buttonResetClicked() {
  graph.reset();
  draw_sketch()
}

function buttonGenNPointsClicked() {
  let n = min(max(0, int(inputN.value())), maxN);
  inputN.value(n);
  graph.genNPoints(n);
  draw_sketch()
}

function Point(x, y) {
  this.x = x;
  this.y = y;
}

class Graph {
  constructor() {
    this.points = [];
    this.triangulation = [];
    this.points.push(new Point(0, 0));
    this.points.push(new Point(0, maxY));
    this.points.push(new Point(maxX, 0));
    this.points.push(new Point(maxX, maxY));
  }

  genNPoints(n) {
    for (let i = 0; i < n; i++) {
      this.points.push(new Point(random(maxX), random(maxY)));
    }
  }
  
  execute() {
    const startTime = performance.now();
    this.triangulation = new Delaunay().run(this.points);
    const endTime = performance.now();
    resultText.html('Час виконання: ' + str(endTime - startTime) + ' мс');
    nText.html('Кількість точок: ' + str(graph.points.length));
  }

  reset() {
    this.points = [];
    this.triangulation = [];
    this.points.push(new Point(0, 0));
    this.points.push(new Point(0, maxY));
    this.points.push(new Point(maxX, 0));
    this.points.push(new Point(maxX, maxY));
  }

  render() {
    stroke(255, 0, 0);
    this.triangulation.forEach((edge)=>{
      line(edge.start.x, edge.start.y, 
        edge.end.x, edge.end.y);
    })
    stroke(255)
    for (let i = 1; i < this.points.length; i++) {
      circle(this.points[i].x, this.points[i].y, 1)
    }
  }
}

class Delaunay {
  constructor() {
    this.edges = new Set();
  }

  make_edge(start, end) {
    var back, fwd;
    fwd = new Edge(start, end);
    back = new Edge(end, start);
    [fwd.rev, back.rev] = [back, fwd];
    [fwd.next, fwd.prev] = [fwd, fwd];
    [back.next, back.prev] = [back, back];
    this.edges.add(fwd);
    return fwd;
  }

  connect(a, b) {
    var edge;
    edge = this.make_edge(a.end, b.start);
    edge.splice(a.rev.prev);
    edge.rev.splice(b);
    return edge;
  }

  delete_edge(edge) {
    edge.splice(edge.prev);
    edge.rev.splice(edge.rev.prev);
    this.edges.delete(edge);
    this.edges.delete(edge.rev);
  }

  run(points) {
    if (points.length < 2) {
      return [];
    }

    points = points.sort((a,b) => a.x - b.x);
    this.triangulate(points);
    //console.log(this.edges);
    return this.edges;
  }

  triangulate(points) {
    var a, b, base, c, edge, left, left_candidate, left_inside, left_outside, middle, p1, p2, p3, right, right_candidate, right_inside, right_outside, temp, valid_left_candidate, valid_right_candidate;

    if (points.length === 2) {
      edge = this.make_edge(points[0], points[1]);
      return [edge, edge.rev];
    }

    if (points.length === 3) {
      [p1, p2, p3] = [points[0], points[1], points[2]];
      a = this.make_edge(p1, p2);
      b = this.make_edge(p2, p3);
      a.rev.splice(b);

      if (right_of(p3, a)) {
        this.connect(b, a);
        return [a, b.rev];
      } else {
        if (left_of(p3, a)) {
          c = this.connect(b, a);
          return [c.rev, c];
        } else {
          return [a, b.rev];
        }
      }
    }

    middle = Math.floor(points.length / 2);
    [left, right] = [points.slice(0, middle), points.slice(middle)];
    [left_outside, left_inside] = this.triangulate(left);
    [right_inside, right_outside] = this.triangulate(right);

    while (true) {
      if (right_of(right_inside.start, left_inside)) {
        left_inside = left_inside.rev.next;
      } else {
        if (left_of(left_inside.start, right_inside)) {
          right_inside = right_inside.rev.prev;
        } else {
          break;
        }
      }
    }

    base = this.connect(left_inside.rev, right_inside);

    if (left_inside.start === left_outside.start) {
      left_outside = base;
    }

    if (right_inside.start === right_outside.start) {
      right_outside = base.rev;
    }

    while (true) {
      [right_candidate, left_candidate] = [base.rev.next, base.prev];
      [valid_right_candidate, valid_left_candidate] = [right_of(right_candidate.end, base), right_of(left_candidate.end, base)];

      if (!(valid_right_candidate || valid_left_candidate)) {
        break;
      }

      if (valid_right_candidate) {
        while (right_of(right_candidate.next.end, base) && in_circle(base.end, base.start, right_candidate.end, right_candidate.next.end)) {
          temp = right_candidate.next;
          this.delete_edge(right_candidate);
          right_candidate = temp;
        }
      }

      if (valid_left_candidate) {
        while (right_of(left_candidate.prev.end, base) && in_circle(base.end, base.start, left_candidate.end, left_candidate.prev.end)) {
          temp = left_candidate.prev;
          this.delete_edge(left_candidate);
          left_candidate = temp;
        }
      }

      if (!valid_right_candidate || valid_left_candidate && in_circle(right_candidate.end, right_candidate.start, left_candidate.start, left_candidate.end)) {
        base = this.connect(left_candidate, base.rev);
      } else {
        base = this.connect(base.rev, right_candidate.rev);
      }
    }

    return [left_outside, right_outside];
  }

}

class Edge {
  constructor(start, end) {
    this.start = start;
    this.end = end;
    this.next = null;
    this.prev = null;
    this.rev = null;
  }

  splice(other) {
    if (this === other) {
      return;
    }

    [this.next.prev, other.next.prev] = [other, this];
    [this.next, other.next] = [other.next, this.next];
  }

}

function in_circle(a, b, c, d) {
  var a1, a2, a3, b1, b2, b3, c1, c2, c3, det;
  [a1, a2] = [a.x - d.x, a.y - d.y];
  [b1, b2] = [b.x - d.x, b.y - d.y];
  [c1, c2] = [c.x - d.x, c.y - d.y];
  [a3, b3, c3] = [Math.pow(a1, 2) + Math.pow(a2, 2), Math.pow(b1, 2) + Math.pow(b2, 2), Math.pow(c1, 2) + Math.pow(c2, 2)];
  det = a1 * b2 * c3 + a2 * b3 * c1 + a3 * b1 * c2 - (a3 * b2 * c1 + a1 * b3 * c2 + a2 * b1 * c3);
  return det < 0;
}

function right_of(p, edge) {
  var a, b, det;
  [a, b] = [edge.start, edge.end];
  det = (a.x - p.x) * (b.y - p.y) - (a.y - p.y) * (b.x - p.x);
  return det > 0;
}

function left_of(p, edge) {
  var a, b, det;
  [a, b] = [edge.start, edge.end];
  det = (a.x - p.x) * (b.y - p.y) - (a.y - p.y) * (b.x - p.x);
  return det < 0;
}
