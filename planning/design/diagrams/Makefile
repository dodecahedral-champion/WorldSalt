DOTFILES = $(wildcard *.dot)
DIAGRAMS = $(patsubst %.dot,%.png,$(DOTFILES))
SHOWS = $(patsubst %,show-%,$(DIAGRAMS))

all: $(DIAGRAMS)

show: $(SHOWS)

clean:
	-@rm $(DIAGRAMS)

spotless: clean
	-@rm *~

%.png: %.dot
	dot -Tpng $< > $@

show-%.png: %.png
	display $< &
