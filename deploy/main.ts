import { Construct } from 'constructs';
import { App, Chart } from 'cdk8s';
import { WebService } from './lib/web-service';
export class MyChart extends Chart {
  constructor(scope: Construct, name: string) {
    super(scope, name);

    
    new WebService(this, 'query-handler', { 
      name: "query-handler",
      image: 'localhost:5000/query-handler:latest'
    });
    
    new WebService(this, 'query-proxy', { 
      name: "query-proxy",
      image: 'localhost:5000/query-proxy:latest'
    });


    new WebService(this, 'command-handler', { 
      name: "command-handler",
      image: 'localhost:5000/command-handler:latest'
    });
    
    new WebService(this, 'command-proxy', { 
      name: "command-proxy",
      image: 'localhost:5000/command-proxy:latest'
    });


    new WebService(this, 'gateway', { 
      name: "gateway",
      image: 'localhost:5000/gateway:latest'
    });
  }
}

const app = new App();
new MyChart(app, 'todo');
app.synth();
