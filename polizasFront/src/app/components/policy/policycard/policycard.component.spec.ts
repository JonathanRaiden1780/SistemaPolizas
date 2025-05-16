import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PolicycardComponent } from './policycard.component';

describe('PolicycardComponent', () => {
  let component: PolicycardComponent;
  let fixture: ComponentFixture<PolicycardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PolicycardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PolicycardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
