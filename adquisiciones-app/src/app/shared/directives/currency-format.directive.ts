import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';
import { CurrencyPipe } from '@angular/common';

@Directive({
  selector: '[appCurrencyFormat]',
  standalone: true,
  providers: [CurrencyPipe]
})
export class CurrencyFormatDirective {
  constructor(private el: ElementRef, private renderer: Renderer2, private currencyPipe: CurrencyPipe) {}

  @HostListener('focus', ['$event.target.value'])
  onFocus(value: string) {
    this.renderer.setProperty(this.el.nativeElement, 'value', this.parse(value));
  }

  @HostListener('blur', ['$event.target.value'])
  onBlur(value: string) {
    const parsedValue = this.parse(value);
    this.renderer.setProperty(this.el.nativeElement, 'value', this.format(parsedValue));
  }

  private format(value: string): string {
    const numberValue = parseFloat(value);
    if (isNaN(numberValue)) return '';
    return this.currencyPipe.transform(numberValue, 'USD', '', '1.0-0') || '';
  }

  private parse(value: string): string {
    return value.replace(/[^0-9.]/g, '');
  }
}
